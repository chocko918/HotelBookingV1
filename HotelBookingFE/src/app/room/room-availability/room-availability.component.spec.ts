import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomAvailabilityComponent } from './room-availability.component';

describe('RoomAvailabilityComponent', () => {
  let component: RoomAvailabilityComponent;
  let fixture: ComponentFixture<RoomAvailabilityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RoomAvailabilityComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RoomAvailabilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
