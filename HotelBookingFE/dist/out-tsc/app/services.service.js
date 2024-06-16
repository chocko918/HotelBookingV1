import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { formatDate } from '@angular/common';
let ServicesService = class ServicesService {
    constructor(https) {
        this.https = https;
        this.APIUrl = "https://localhost:44311";
    }
    getCustomerList() {
        return this.https.get(this.APIUrl + '/customer')
            .pipe(catchError(this.handleError));
    }
    login(email, password) {
        return this.https.post(`${this.APIUrl}/login`, { email, password })
            .pipe(catchError(this.handleError));
    }
    getRoomList() {
        return this.https.get(this.APIUrl + '/Rooms')
            .pipe(catchError(this.handleError));
    }
    getAvailableRooms(pax, checkInDate, checkOutDate) {
        const formattedCheckInDate = formatDate(checkInDate, 'yyyy-MM-dd', 'en-US');
        const formattedCheckOutDate = formatDate(checkOutDate, 'yyyy-MM-dd', 'en-US');
        const params = new HttpParams()
            .set('pax', pax.toString())
            .set('checkInDate', formattedCheckInDate)
            .set('checkOutDate', formattedCheckOutDate);
        console.log('Request URL:', `${this.APIUrl}/available-rooms`, { params });
        return this.https.get(`${this.APIUrl}/available-rooms`, { params }).pipe(catchError(this.handleError));
    }
    addItemToCart(roomID, checkInDate1, checkOutDate1) {
        const checkInDate = formatDate(checkInDate1, 'yyyy-MM-dd', 'en-US');
        const checkOutDate = formatDate(checkOutDate1, 'yyyy-MM-dd', 'en-US');
        //const params = new HttpParams()
        //  .set('roomID', roomID.toString())
        //  .set('checkInDate', formattedCheckInDate)
        //  .set('checkOutDate', formattedCheckOutDate);
        //console.log(roomID, checkInDate, checkOutDate);
        const params = {
            roomID,
            checkInDate,
            checkOutDate
        };
        //const payload = {
        //  RoomID: RoomId.toString(),
        //  checkInDate: checkInDate.toString(),
        //  checkOutDate: checkOutDate.toString()
        //};
        console.log('Request URL:', `${this.APIUrl}/addItemToCart`, params);
        return this.https.post(`${this.APIUrl}/addItemToCart`, params).pipe(catchError(this.handleError));
    }
    getAllCartItems() {
        return this.https.get(this.APIUrl + '/getAllCartItems')
            .pipe(catchError(this.handleError));
    }
    deleteCartItem(itemID) {
        return this.https.delete(this.APIUrl + '/DeleteCartItem', { params: { itemID } });
    }
    confirmCartItem(customerID) {
        return this.https.post(`${this.APIUrl}/confirm`, { customerID })
            .pipe(catchError(this.handleError));
    }
    handleError(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred.
            errorMessage = `An error occurred: ${error.error.message}`;
        }
        else {
            // The backend returned an unsuccessful response code.
            errorMessage = error.error.message || `Server returned code: ${error.status}, error message is: ${error.message}`;
        }
        console.error(errorMessage);
        return throwError({ Message: errorMessage });
    }
};
ServicesService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], ServicesService);
export { ServicesService };
//# sourceMappingURL=services.service.js.map