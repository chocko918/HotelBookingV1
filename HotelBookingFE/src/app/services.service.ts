import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse,HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { formatDate } from '@angular/common';


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

  addItemToCart(roomID: number, checkInDate: Date, checkOutDate: Date): Observable<any> {
    const formattedCheckInDate = formatDate(checkInDate, 'yyyy-MM-dd', 'en-US');
    const formattedCheckOutDate = formatDate(checkOutDate, 'yyyy-MM-dd', 'en-US');
    //const params = new HttpParams()
    //  .set('roomID', roomID.toString())
    //  .set('checkInDate', formattedCheckInDate)
    //  .set('checkOutDate', formattedCheckOutDate);
    //console.log(roomID, checkInDate, checkOutDate);
    const params = {
      roomID,
      formattedCheckInDate,
      formattedCheckOutDate
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
