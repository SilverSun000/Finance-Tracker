using System;

class Program {
    static void Main(string[] args) {
        User test = new User();
        test.createNewUser();
        test.showUserInfo();
        test.Login(test.Username, test.Password);
    }
}