import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
    private readonly http = inject(HttpClient);
    private readonly baseUrl = environment.apiUrl;

    getAll<T>(route: string): Observable<T> {
        return this.http.get<T>(`${this.baseUrl}/${route}`);
    }

    insert<T,R>(route: string, data: T): Observable<R> {
        return this.http.post<R>(`${this.baseUrl}/${route}`, data);
    }

    update<T,R>(route: string, id: number, data: T): Observable<R> {
        return this.http.put<R>(`${this.baseUrl}/${route}/${id}`, data);
    }

    delete<R>(route: string, id: number): Observable<R> {
        return this.http.delete<R>(`${this.baseUrl}/${route}/${id}`);
    }
}
