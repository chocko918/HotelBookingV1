import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerLoginPageComponent } from './customer-login-page.component';

describe('CustomerLoginPageComponent', () => {
  let component: CustomerLoginPageComponent;
  let fixture: ComponentFixture<CustomerLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomerLoginPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
