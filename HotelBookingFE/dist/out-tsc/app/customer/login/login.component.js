import { __decorate } from "tslib";
import { Component } from '@angular/core';
let LoginComponent = class LoginComponent {
    //customer: any; // Variable to hold customer details
    constructor(authService, router, cookieService // Inject CookieService
    ) {
        this.authService = authService;
        this.router = router;
        this.cookieService = cookieService;
        this.errorMessage = "";
    }
    login() {
        console.log(this.email, this.password);
        //this.authService.login(this.email, this.password)
        //  .subscribe(
        //    () => {
        //      // Redirect to dashboard or any other page on successful login
        //      console.log('Success: ' + this.router.url); // Log URL on success
        //      this.router.navigate(['/dashboard']);
        //    },
        //    error => {
        //      this.errorMessage = error; // Display error message on login page
        //      console.log('Failure: ' + this.router.url); // Log URL on failure
        //    }
        //);
        this.authService.login(this.email, this.password).subscribe(user => {
            this.errorMessage = '';
            // Handle successful login
            this.cookieService.set('customerID', user.customerID);
            console.log("Login successful, token: ", user);
            this.router.navigate(['/loginpage'], { state: { token: user.token, user: user.user } });
        }, error => {
            // Handle login error
            console.error("Login error: ", error);
            this.errorMessage = error.Message;
        });
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map