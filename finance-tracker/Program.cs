using System;

class Program {
    static void Main(string[] args) {
        User test = new User();
        test.createNewUser();
        test.showUserInfo();
        test.Login(test.Username, test.Password);

        using(StreamWriter writer = new StreamWriter("C:/Users/Billson/Documents/Github/Finance-Tracker/userCredentials.txt")) {
            writer.WriteLine("beans beans beans \n" +
                "also another test."); 
        }
    }
}