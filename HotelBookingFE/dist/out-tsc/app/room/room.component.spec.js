import { TestBed } from '@angular/core/testing';
import { RoomComponent } from './room.component';
describe('RoomComponent', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [RoomComponent]
        })
            .compileComponents();
        fixture = TestBed.createComponent(RoomComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=room.component.spec.js.map