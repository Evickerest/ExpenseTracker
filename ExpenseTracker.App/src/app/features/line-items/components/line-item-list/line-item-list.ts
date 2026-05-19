import { Component, computed, inject, linkedSignal } from '@angular/core';
import { ApiService } from '../../../../shared/services/api-service';
import { rxResource } from '@angular/core/rxjs-interop';
import { LineItem } from '../../models/line-item';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-line-item-list',
  imports: [DatePipe],
  templateUrl: './line-item-list.html',
  styleUrl: './line-item-list.css',
})
export class LineItemList {
    private readonly apiService = inject(ApiService);

    private readonly lineResource = rxResource({
        stream: () => this.apiService.getAll<LineItem[]>("line-items"),
        defaultValue: []
    });

    protected readonly isLoading = computed(() => this.lineResource.isLoading());
    protected readonly hasError = computed(() => this.lineResource.error());
    protected readonly lines = linkedSignal(() => this.lineResource.value());
}
