import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import {
  faEdit,
  faTrash,
  faPlus,
  faSearch,
  faRotate,
  faAngleLeft,
  faAngleRight,
  faAnglesRight,
  faAnglesLeft,
} from '@fortawesome/free-solid-svg-icons';
import { PaginatedResult } from '../../models/paginated-result.model';
import { TableColumn } from '../../models/table-column.model';

@Component({
  selector: 'app-table',
  imports: [CommonModule, FontAwesomeModule, FormsModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent {
  //#region Font Awesome icons
  public faEdit: IconDefinition = faEdit;
  public faTrash: IconDefinition = faTrash;
  public faPlus: IconDefinition = faPlus;
  public faSearch: IconDefinition = faSearch;
  public faRotate: IconDefinition = faRotate;
  public faAngleLeft: IconDefinition = faAngleLeft;
  public faAnglesLeft: IconDefinition = faAnglesLeft;
  public faAngleRight: IconDefinition = faAngleRight;
  public faAnglesRight: IconDefinition = faAnglesRight;

  //#endregion

  @Input() columns: TableColumn[] = [];
  @Input() public isShowNumber?: boolean = true;
  @Input() public editable?: boolean = true;

  @Input() public currentPage: number = 1;
  @Input() public currentPageSize: number = 10;

  @Input() public data!: PaginatedResult<any>;

  @Input() public pageSizeOptions: number[] = [5, 10, 25, 50, 100];

  @Output() public onEdit: EventEmitter<string> = new EventEmitter<string>();

  @Output() public onDelete: EventEmitter<string> = new EventEmitter<string>();

  @Output() public onPageSizeChange: EventEmitter<any> =
    new EventEmitter<any>();

  @Output() public onPageChange: EventEmitter<number> =
    new EventEmitter<number>();
}
