import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService, MessageService } from '../services';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private messageService: MessageService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      if (err.status === 400) {
        let errText: string='';
        if (err.error.hasOwnProperty('errors')) {
          var prop = Object.getOwnPropertyNames(err.error.errors);
          prop.forEach((item) => {
            err.error.errors[item].forEach((it) => {
              errText += `${item}: ${it}  `;
            });
          });
        } else errText = err.error;
        this.messageService.showError(errText, err.statusText);
      }
      if (err.status === 401) {
        //this.authService.logout();
        //location.reload(true);
      }
      if (err.status === 500) {
        this.messageService.showError("Internal Server Error");
      }
      
      return throwError(err);
    }));
  }
}
