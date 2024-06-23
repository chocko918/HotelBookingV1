import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CustomerComponent } from './customer/customer.component';
import { LoginComponent } from './customer/login/login.component';
import { CustomerLoginPageComponent } from './customer/login/customer-login-page/customer-login-page.component';
import { RoomComponent } from './room/room.component';
import { RoomAvailabilityComponent } from './room/room-availability/room-availability.component';
import { CartComponent } from './cart/cart.component';
import { RegisterComponent } from './customer/register/register.component';


const routes: Routes = [

  { path: 'customer', component: CustomerComponent },
  { path: 'login', component: LoginComponent },
  { path: 'loginpage', component: CustomerLoginPageComponent },
  { path: 'room', component: RoomComponent },
  { path: 'roomAvailability', component: RoomAvailabilityComponent },
  { path: 'cart', component: CartComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: '/room', pathMatch: 'full' }, // Redirect to room component by default

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
