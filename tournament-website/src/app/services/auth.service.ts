import { LoginRequest } from './../models/login-request.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url = `https://localhost:44355/auth`;

  constructor(private http: HttpClient) {}

  signIn(loginRequest: LoginRequest): Observable<any[]> {
      Swal.showLoading();
      return this.http.post<any>(`${this.url}/login`, JSON.stringify(loginRequest))
     .pipe(
        tap((response: any) => {
          this.saveAuthData(response.token);
          return of(true);
        })
    );
  }

  signOut(): void{
    localStorage.removeItem('access_token');
  }

  private saveAuthData(token: string): void {
      localStorage.setItem('access_token', token);
  }
}
