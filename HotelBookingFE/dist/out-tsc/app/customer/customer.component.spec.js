import { TestBed } from '@angular/core/testing';
import { CustomerComponent } from './customer.component';
describe('CustomerComponent', () => {
    let component;
    let fixture;
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [CustomerComponent]
        })
            .compileComponents();
        fixture = TestBed.createComponent(CustomerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=customer.component.spec.js.map