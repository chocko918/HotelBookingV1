import { TestBed } from '@angular/core/testing';
import { ServicesService } from './services.service';
describe('ServicesService', () => {
    let service;
    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(ServicesService);
    });
    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=services.service.spec.js.map