using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodCastAppFormVersion.Services
{


    /*
     *  client.SetTaskAsync(path, data)
        client.GetTaskAsync(path)
        client.PushTaskAsync(path, data)
        client.DeleteTaskAsync(path)
        client.UpdateTaskAsync(path,data)
     */

    public class DatabaseService
    {

        IFirebaseClient client;

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "sxiLf3Fpv7F2RQKjHZ3cdgqzYvnSd6nw6i4VBXjN",
            BasePath = "https://podcasthackathonforms.firebaseio.com/"
        };

        public DatabaseService()
        {
            client = new FireSharp.FirebaseClient(config);
        }

        public bool isConnectionEstablished()
        {
            if (client == null)
                return false;
            return true;
        }

        public async Task<User> addNewUser(String ID, User data)
        {
            FirebaseResponse response = await client.GetTaskAsync("UserToIDMap/" + data.Name);
            SetResponse respponse;
            UserIDMap UIM;

            if (response.Body == "null")
            {
                UIM = new UserIDMap();
                UIM.Name = data.Name;
                UIM.ID = new List<string>();
                UIM.ID.Add(ID);
                respponse = await client.SetTaskAsync("UserToIDMap/" + UIM.Name, UIM);
            }
            else
            {
                UIM = response.ResultAs<UserIDMap>();
                UIM.ID.Add(ID);
                await client.UpdateTaskAsync("UserToIDMap/" + UIM.Name, UIM);
            }

            SetResponse response1 = await client.SetTaskAsync("UserInformation/" + ID, data);
            User result = response1.ResultAs<User>();
            return result;
        }

        public async Task<List<String>> getAllFriends(String ID)
        {
            FirebaseResponse response = await client.GetTaskAsync("UserInformation/" + ID +"/friends");
            return response.ResultAs<List<String>>();
        }
        public async Task<bool> addNewFriend(String IDUser, String IDFriend)
        {
            User user = await getUser(IDUser);
            user.friends.Add(IDFriend);
            SetResponse response = await client.SetTaskAsync("UserInformation/" + IDUser, user);
            if (response.Exception != null)
                return true;

            return false;
        }

        public async Task<List<String>> getUserIds(string name)
        {
            FirebaseResponse response = await client.GetTaskAsync("UserToIDMap/" + name);
            UserIDMap UIM = response.ResultAs<UserIDMap>();
            return UIM.ID;
        }

        public async Task<List<User>> getUsersByName(string name)
        {

            List<User> userList = new List<User>();
            List<String> ids = await getUserIds(name);
            foreach (String id in ids)
            {
                userList.Add(await getUser(id));
            }
            return userList;
        }

        public async Task<User> getUser(string ID)
        {
            FirebaseResponse response = await client.GetTaskAsync("UserInformation/"+ID);
            if(response!=null)
            {
                return response.ResultAs<User>();
            }
            return null;
        }
  //      public void doesUserExist();

        public async Task<bool> areTheyfriends(string IDUser, string IDFriend)
        {
            List<String> friendList = await getAllFriends(IDUser);
            return friendList.Contains(IDFriend);
        }

        public async Task<String> getUserAbout(String ID)
        {
            FirebaseResponse response = await client.GetTaskAsync("UserInformation/"+ID);
            if (response != null)
            {
                return response.ResultAs<User>().About;
            }
            return null;
        }

        //public bool addPodcast();
//        public bool notifyFriends();

    }
}
