import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse,HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { formatDate } from '@angular/common';
import { CartResponse } from './cart/cart.model';
import { CreateCustomerDTO } from './customer/register/register.model';


@Injectable({
  providedIn: 'root'
})
export class ServicesService {

  readonly APIUrl = "https://localhost:44311";

  constructor(private https: HttpClient) { }

  getCustomerList(): Observable<any[]> {
    return this.https.get<any[]>(this.APIUrl + '/customer')
      .pipe(
        catchError(this.handleError)
      );
  }

  login(email: string, password: string): Observable<any> {
    return this.https.post<any>(`${this.APIUrl}/login`, { email, password })
      .pipe(
        catchError(this.handleError)
      );
  }

  getRoomList(): Observable<any[]> {
    return this.https.get<any[]>(this.APIUrl + '/Rooms')
      .pipe(
        catchError(this.handleError)
      );
  }

  getAvailableRooms(pax: number, checkInDate: Date, checkOutDate: Date): Observable<any> {
    const formattedCheckInDate = formatDate(checkInDate, 'yyyy-MM-dd', 'en-US');
    const formattedCheckOutDate = formatDate(checkOutDate, 'yyyy-MM-dd', 'en-US');
    const params = new HttpParams()
      .set('pax', pax.toString())
      .set('checkInDate', formattedCheckInDate)
      .set('checkOutDate', formattedCheckOutDate);

    console.log('Request URL:', `${this.APIUrl}/available-rooms`, { params });
    return this.https.get<any>(`${this.APIUrl}/available-rooms`, { params }).pipe(
      catchError(this.handleError)
    );
  }

  addItemToCart(customerID: string,roomID: number, checkInDate1: Date, checkOutDate1: Date): Observable<any> {
    const checkInDate = formatDate(checkInDate1, 'yyyy-MM-dd', 'en-US');
    const checkOutDate = formatDate(checkOutDate1, 'yyyy-MM-dd', 'en-US');
    //const params = new HttpParams()
    //  .set('roomID', roomID.toString())
    //  .set('checkInDate', formattedCheckInDate)
    //  .set('checkOutDate', formattedCheckOutDate);
    //console.log(roomID, checkInDate, checkOutDate);
    const params = {
      customerID,
      roomID,
      checkInDate,
      checkOutDate
    }
    //const payload = {
    //  RoomID: RoomId.toString(),
    //  checkInDate: checkInDate.toString(),
    //  checkOutDate: checkOutDate.toString()
    //};
    console.log('Request URL:', `${this.APIUrl}/addItemToCart`,  params );
    return this.https.post<any>(`${this.APIUrl}/addItemToCart`,  params ).pipe(
      catchError(this.handleError)

    );
  }

  getAllCartItems(): Observable<CartResponse> {
    return this.https.get<CartResponse>(this.APIUrl + '/getAllCartItems')
      .pipe(
        catchError(this.handleError)
      );
  }

  deleteCartItem(itemID: string): Observable<any> {
    return this.https.delete(this.APIUrl + '/DeleteCartItem', { params: { itemID } });
  }


  
  confirmCartItem(customerID: string): Observable<any> {
    return this.https.post<any>(`${this.APIUrl}/confirm`, { customerID }).pipe(
      catchError(this.handleError)
    );
  }



  getBookingsByCustomerID(customerID: string): Observable<any> {

    return this.https.post<any>(`${this.APIUrl}/getBookingByCustomerID`, { customerID }).pipe(
      catchError(this.handleError)
    );
  }

  logout(): Observable<any> {
    // Call the logout endpoint if needed
    return this.https.post<any>(`${this.APIUrl}/logout`, { }).pipe(
      catchError(this.handleError)
    );
  }

  deleteAllCartItems(): Observable<any> {
    // Call the logout endpoint if needed
    return this.https.delete<any>(`${this.APIUrl}/DeleteAllCartItem`, {}).pipe(
      catchError(this.handleError)
    );
  }




  deleteCartItemByCustomerId(customerID: string): Observable<any> {
    const params = {
      customerID,

    }
    return this.https.delete(this.APIUrl + '/DeleteAllCartItemByCustomerId', { params });
  }

  getCartItemsByCustomerID(customerID: string): Observable<any> {
/*    const params = new HttpParams().set('customerID', customerID);*/
    const params = {
      customerID,

    }
    return this.https.get(`${this.APIUrl}/getCartItemsByCustomerID`, { params });
  }

  


  //register(customer: CreateCustomerDTO): Observable<any> {
  //  console.log(customer.birthday)
  //  console.log(customer)
  //  //customer.birthday = new Date(customer.birthday).toISOString().split('T')[0];
  //  console.log(this.https.post<any>(`${this.APIUrl}/register`, { customer } ));
  //  return this.https.post<any>(`${this.APIUrl}/register`,  customer ).pipe(
  //    catchError(this.handleError)
  //  );
  //}

  //register(customer: CreateCustomerDTO): Observable<any> {
  //  const formatDate = (date: Date): string => {
  //    const year = date.getFullYear();
  //    const month = (date.getMonth() + 1).toString().padStart(2, '0');
  //    const day = date.getDate().toString().padStart(2, '0');
  //    return `${year}-${month}-${day}T00:00:00`;
  //  };

  //  const formattedCustomer = {
  //    ...customer,
  //    birthday: formatDate(new Date(customer.birthday))
  //  };

  //  console.log(formattedCustomer);

  //  return this.https.post<any>(`${this.APIUrl}/register`, formattedCustomer).pipe(
  //    catchError(this.handleError)
  //  );
  //}

  register(customer: CreateCustomerDTO): Observable<any> {
    console.log(customer)
    return this.https.post<any>(`${this.APIUrl}/register`, customer).pipe(
      catchError(this.handleError)
    );
  }



  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred.
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      errorMessage = error.error.message || `Server returned code: ${error.status}, error message is: ${error.message}`;
    }
    console.error(errorMessage);
    return throwError({ Message: errorMessage });
  }
}
