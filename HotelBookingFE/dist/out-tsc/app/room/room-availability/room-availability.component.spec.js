import { TestBed } from '@angular/core/testing';
import { RoomAvailabilityComponent } from './room-availability.component';
describe('RoomAvailabilityComponent', () => {
    let component;
    let fixture;
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
//# sourceMappingURL=room-availability.component.spec.js.map