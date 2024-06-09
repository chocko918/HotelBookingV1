import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesService } from '../../../services.service'; // Import your authentication service
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-customer-login-page',
  templateUrl: './customer-login-page.component.html',
  styleUrl: './customer-login-page.component.css'
})
export class CustomerLoginPageComponent implements OnInit {
  token: string = '';
  user: any = {};

  constructor(private route: Router) { }

  ngOnInit(): void {
    // Extract token and user information from route parameters
    const state = history.state;
    if (state && state.token && state.user) {
      this.token = state.token;
      this.user = state.user;
      console.log(this.user)
    }
  }
}
