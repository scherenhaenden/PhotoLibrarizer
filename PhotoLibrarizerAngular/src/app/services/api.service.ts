import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private readonly apiUrl = environment.apiUrl;
  private readonly defaultHeaders = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  public post<T, U>(url: string, body: U, headers?: HttpHeaders, params?: HttpParams): Observable<T> {
    return this.http.post<T>(this.buildUrl(url), body, this.createHttpOptions(headers, params))
      .pipe(catchError(this.handleError));
  }

  public put<T, U>(url: string, body: U, headers?: HttpHeaders, paramsOrModel?: HttpParams | Record<string, any>): Observable<T> {
    return this.http.put<T>(this.buildUrl(url), body, this.createHttpOptions(headers, this.convertToHttpParams(paramsOrModel)))
      .pipe(catchError(this.handleError));
  }

  public delete<T>(url: string, headers?: HttpHeaders, params?: HttpParams): Observable<T> {
    return this.http.delete<T>(this.buildUrl(url), this.createHttpOptions(headers, params))
      .pipe(catchError(this.handleError));
  }

  public get<T, R extends Record<string, any> | undefined>(url: string, paramsOrModel?: HttpParams | R, headers?: HttpHeaders): Observable<T> {
    return this.http.get<T>(this.buildUrl(url), this.createHttpOptions(headers, this.convertToHttpParams(paramsOrModel)))
      .pipe(catchError(this.handleError));
  }

  private buildUrl(endpoint: string): string {
    return `${this.apiUrl}${endpoint}`;
  }

  private createHttpOptions(headers?: HttpHeaders, params?: HttpParams) {
    return {
      headers: headers ?? this.defaultHeaders,
      params: params
    };
  }

  private convertToHttpParams(paramsOrModel?: HttpParams | Record<string, any>): HttpParams | undefined {
    if (!paramsOrModel) return undefined;
    return paramsOrModel instanceof HttpParams ? paramsOrModel : new HttpParams({ fromObject: paramsOrModel });
  }

  private handleError(error: HttpErrorResponse) {
    const errorMsg = error.error instanceof ErrorEvent
      ? `An error occurred: ${error.error.message}`
      : `Backend returned code ${error.status}, body was: ${error.error}`;
    console.error(errorMsg);
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
