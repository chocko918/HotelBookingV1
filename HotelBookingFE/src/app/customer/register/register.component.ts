import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ServicesService } from '../../services.service'; // Import your authentication service
import { Router, NavigationEnd } from '@angular/router';
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { CreateCustomerDTO} from './register.model'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent {
  registerForm: FormGroup;
  maxDate: string;
  serverErrorMessages: string[] = [];

  constructor(private fb: FormBuilder, private authService: ServicesService) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      customerName: ['', [Validators.required, Validators.pattern('^[a-zA-Z\\s]+$')]],
      birthday: ['', [Validators.required, this.minimumAgeValidator(18)]]
    });
    this.maxDate = this.calculateMaxDate();
  }

  calculateMaxDate(): string {
    const today = new Date();
    const maxDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
    return maxDate.toISOString().split('T')[0];
  }

  minimumAgeValidator(minAge: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const birthDate = new Date(control.value);
      const today = new Date();
      const age = today.getFullYear() - birthDate.getFullYear();
      const monthDifference = today.getMonth() - birthDate.getMonth();

      if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < birthDate.getDate())) {
        return { 'minAge': { requiredAge: minAge, actualAge: age } };
      }

      return age >= minAge ? null : { 'minAge': { requiredAge: minAge, actualAge: age } };
    };
  }

  onSubmit() {
    if (this.registerForm.valid) {

      const formValue = { ...this.registerForm.value };
      //formValue.birthday = new Date(formValue.birthday).toISOString().split('T')[0];
      console.log(formValue);
      this.authService.register(formValue).subscribe(
        (response: any) => {
          console.log('Registration successful', response);
          this.serverErrorMessages = [];
        },
        (error: any) => {
          if (error.status === 400 && error.error && error.error.errors) {
            this.serverErrorMessages = error.error.errors.map((err: any) => err.errorMessage);
          } else {
            this.serverErrorMessages = ["Email exists."];
            console.error('Registration failed', error);
          }
        });
    }
  }
}
