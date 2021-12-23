


using System;
using System.Threading;

using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//place other needed directives
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField Username;
    public TMP_InputField Email;
    public TMP_InputField Password;
    
    public Button EnterButton;
    
    public Firebase.Auth.FirebaseAuth auth;
    public DatabaseReference dbReference;


    //initialise our auth instance
    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        DontDestroyOnLoad(this.gameObject);
    } 

    public void SignUpButton()
    {
        SignUpNewUser();
    }

    public void SignInButton()
    {
        SignInUser(Email.text, Password.text);
    }

    public void LoadGame()
    {
        //Load the GameScene here
        //SceneManager.LoadScene("SimpleSmasher");
    }


    public async void SignUpNewUser()
    {
        string email = Email.text;
        string password = Password.text;

        FirebaseUser newPlayer = await SignUpNewUserOnly(email, password);
        string userName = Username.text;
        if(newPlayer != null){
            await CreateNewSimplePlayer(newPlayer.UserId, userName, email);
            await UpdatePlayerDisplayName(userName);
            SceneManager.LoadScene("MainMenu");
        }
    }

    //methods to handle authentication
    public async Task<FirebaseUser> SignUpNewUserOnly(string email, string password)
    {

        FirebaseUser newPlayer = null;
        //automatically pass user info the firebase project
        //attempt to create new user or check if theres already one
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => 
        {
            if (task.IsCanceled) 
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) 
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                // Firebase user has been created.
                newPlayer = task.Result;
                
                Debug.LogFormat("New Player Details Added to Database {0}. {1}", newPlayer.UserId, newPlayer.Email);      
            }
            });
            return newPlayer;
           
        }
    public async Task UpdatePlayerDisplayName(string userName)
    {
        if(auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile 
            {
                DisplayName = userName
            };
            
            await auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread( task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync has been cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync has encounted an error" + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully");
                Debug.LogFormat("Checking current user display name from auth {0}", GetCurrentDisplayName() );
                
            });
        }
    }

    public async Task CreateNewSimplePlayer(string UserId, string userName, string email, 
        int lastcp = 0)
    {
        SimplePlayer newPlayer = new SimplePlayer(UserId, userName, email, lastcp);
        Debug.LogFormat("Player Details : {0}", newPlayer.PrintPlayerDetails());
        
        await dbReference.Child("users/" + UserId).SetRawJsonValueAsync(newPlayer.SaveToJson());


        //update auth player with new display name -> tagging along the username input field
        await UpdatePlayerDisplayName(userName);
        
    }

    public string GetCurrentDisplayName()
    {
        return auth.CurrentUser.DisplayName;
    }


    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => 
        {

            if (task.IsCanceled) 
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) 
            {
                Debug.LogError("SignInrWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.FirebaseUser newPlayer = task.Result;  
                Debug.LogFormat("User signed in successfully: {0} {1}", newPlayer.DisplayName, newPlayer.UserId);
                SceneManager.LoadScene("MainMenu");
            }    
        });
        //LoadGame();
    }

    public void SignOutUser()
    {
        Debug.Log("Signing Out");
        if (auth.CurrentUser != null) 
        {
            Debug.LogFormat("Auth user {0} {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);

            auth.SignOut();
            SceneManager.LoadScene("Authenticate");
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
