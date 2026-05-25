import { inject, Injectable } from '@angular/core';
import { ApiService } from '../../../shared/services/api-service';
import { catchError, Observable, of, switchMap } from 'rxjs';
import { LineItem, LineItemUpdate } from '../models/line-item';

@Injectable({
  providedIn: 'root',
})
export class LineItemService {
    private readonly apiService = inject(ApiService);

    public getLines(): Observable<LineItem[]> {
        return this.apiService.getAll<LineItem[]>("line-items");
    }
    
    public createLine(line: LineItem): Observable<LineItem | null> {
        const dto = {
            amount: line.amount, type: line.type, timestamp: line.timestamp, description: line.description
        } as LineItemUpdate;

        return this.apiService.insert<LineItemUpdate, LineItem>("line-items", dto).pipe(
            catchError(() => of(null))
        );
    }

    public updateLine(line: LineItemUpdate, id: number): Observable<boolean> {
        const dto = {
            amount: line.amount, type: line.type, timestamp: line.timestamp, description: line.description
        } as LineItemUpdate;

        return this.apiService.update("line-items", id, dto).pipe(
            switchMap(() => of(true)),
            catchError(() => of(false))
        );
    }

    public deleteLine(id: number): Observable<boolean> {
        return this.apiService.delete("line-items", id).pipe(
            switchMap(() => of(true)),
            catchError(() => of(false))
        );
    }
}
