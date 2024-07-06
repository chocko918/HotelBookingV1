import { Component } from '@angular/core';
import { ServicesService } from '../../services.service'; // Import your authentication service
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service'; // Import CookieService

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  email!: string;
  password!: string;
  errorMessage: string = "" ;
  //customer: any; // Variable to hold customer details

  constructor(
    private authService: ServicesService,
    private router: Router,
    private cookieService: CookieService // Inject CookieService
  ) { }

  login() {
     console.log(this.email, this.password)

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
    this.authService.login(this.email, this.password).subscribe(
      user => {
        this.errorMessage = '';
        // Handle successful login
        this.cookieService.set('customerID', user.user.customerID);
        this.cookieService.set('token', user.token);
        this.cookieService.set('user', user.user);
        this.cookieService.set('email', user.user.email);
        this.cookieService.set('customerName', user.user.customerName);
        this.cookieService.set('birthday', user.user.birthday);
        console.log(user.user.customerID)
        console.log("Login successful, token: ", user.user);
        this.router.navigate(['/loginpage']);
        //this.router.navigate(['/loginpage'], { state: { token: user.token, user: user.user } }); 
      },
      error => {
        // Handle login error
        console.error("Login error: ", error);
        this.errorMessage = error.Message;
      }
    );

  }

  onLogout() {
    this.authService.logout().subscribe(
      () => {
        // Clear the JWT token from localStorage
        this.cookieService.delete('customerID');
        // Redirect to the login page
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Logout error', error);
      }
    );
  }
}

