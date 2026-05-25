import { Component, computed, effect, ElementRef, inject, linkedSignal, signal, viewChild } from '@angular/core';
import { rxResource } from '@angular/core/rxjs-interop';
import { defaultLineItem, LineItem } from '../../models/line-item';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { LineItemService } from '../../services/line-item-service';
import { form, FormField } from '@angular/forms/signals';
import { defaultLineItemSummary } from '../../models/line-item-summary';

@Component({
  selector: 'app-line-item-list',
  imports: [DatePipe, FormField, CurrencyPipe],
  templateUrl: './line-item-list.html',
  styleUrl: './line-item-list.css',
})
export class LineItemList {
    private readonly lineService = inject(LineItemService);
    private readonly editPopup = viewChild.required<ElementRef<HTMLDialogElement>>("editPopup");

    private readonly lineResource = rxResource({
        stream: () => this.lineService.getLines(),
        defaultValue: []
    });
    
    private readonly summaryResource = rxResource({
        stream: () => this.lineService.getSummary(),
        defaultValue: defaultLineItemSummary
    })

    protected readonly linesIsLoading = computed(() => this.lineResource.isLoading());
    protected readonly linesHasError = computed(() => this.lineResource.error());
    protected readonly lines = linkedSignal(() => this.lineResource.value());

    protected readonly summaryIsLoading = computed(() => this.summaryResource.isLoading());
    protected readonly summaryHasError = computed(() => this.summaryResource.error());
    protected readonly summary = computed(() => this.summaryResource.hasValue() ? this.summaryResource.value() : defaultLineItemSummary);

    protected readonly editingLine = signal<LineItem>(defaultLineItem);
    protected readonly editForm = form(this.editingLine);

    protected addLine() {
        const line = defaultLineItem;
        line.timestamp = new Date().toUTCString();
        this.prepareEditForm(line);
    }

    protected editLine(line: LineItem) {
        this.prepareEditForm(line);
    }

    private prepareEditForm(line: LineItem) {
        // Prepare timestamp value to be accepted by input of type datetime-local 
        line.timestamp = this.toLocalDateTime(line.timestamp);
        this.editingLine.set(line);
        this.editPopup().nativeElement.showModal();
    }

    protected submitEditPopup() {
        this.editPopup().nativeElement.close();
        const line = this.editingLine();
        line.timestamp = this.toUtcDateTime(line.timestamp);

        if (line.id === 0) {
            this.createLine(line);
        } else {
            this.updateLine(line);
        }
    }

    private createLine(line: LineItem) {
        this.lineService.createLine(line).subscribe(res => {
            if (!res) {
                alert("Failed to create line.");
            } else {
                this.summaryResource.reload();
                this.lines.update(r => {
                    return [...r, line]
                        .sort((l1, l2) => new Date(l1.timestamp).getTime() - new Date(l2.timestamp).getTime());
                });
            }
        });
    }

    private updateLine(line: LineItem) {
        this.lineService.updateLine(line, line.id).subscribe(res => {
            if (!res) {
                alert("Failed to update line.");
            } else {
                this.summaryResource.reload();
                this.lines.update(r => r.map(l => {
                    if (l.id === line.id) return line;
                    return l;
                }));
            }
        });
    } 
    
    protected deleteLine(line: LineItem) {
        const res = confirm("Are your sure you want to delete this line?");
        if (!res) return;

        this.lineService.deleteLine(line.id).subscribe(res => {
            if (!res) {
                alert("Failed to delete line.");
            } else {
                this.summaryResource.reload();
                this.lines.update(r => r.filter(l => l.id !== line.id));
            }
        });
    }

    protected closeEditPopup() {
        this.editPopup().nativeElement.close();
    }

    private toLocalDateTime(utcDateTime: string): string {
        const date = new Date(utcDateTime);
        if (!date) return "";
        date.setMinutes(date.getMinutes() - date.getTimezoneOffset());

        return date.toISOString().slice(0, 16); // yyyy-MM-ddTHH:MM
    }

    private toUtcDateTime(localDateTime: string): string {
        const date = new Date(localDateTime);
        if (!date) return "";

        return date.toISOString();
    }
}
