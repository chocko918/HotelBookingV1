import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private refreshCartListSource = new Subject<void>();
  refreshCartList$ = this.refreshCartListSource.asObservable();

  triggerRefreshCartList() {
    this.refreshCartListSource.next();
  }
}
