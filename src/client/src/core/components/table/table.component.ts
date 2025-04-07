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
  faInfoCircle,
  faCircleInfo,
  faLock,
  faLockOpen,
} from '@fortawesome/free-solid-svg-icons';
import { PaginatedResult } from '../../models/paginated-result.model';
import { TableColumn } from '../../models/table-column.model';
import { PipesModule } from '../../../pipes/pipe.module';
import { ConfirmModalComponent } from '../../../modules/shared/common/confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [
    CommonModule,
    FontAwesomeModule,
    FormsModule,
    PipesModule,
    ConfirmModalComponent,
  ],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent {
  //#region Font Awesome icons
  public faInfoCircle: IconDefinition = faInfoCircle;
  public faEdit: IconDefinition = faEdit;
  public faTrash: IconDefinition = faTrash;
  public faCircleInfo: IconDefinition = faCircleInfo;
  public faLock: IconDefinition = faLock;
  public faLockOpen: IconDefinition = faLockOpen;

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
  @Input() public deletable?: boolean = true;
  @Input() public infoable?: boolean = true;
  @Input() public lockable?: boolean = true;

  @Input() public currentPage: number = 1;
  @Input() public currentPageSize: number = 10;

  @Input() public data!: PaginatedResult<any>;

  @Output() public onView: EventEmitter<string> = new EventEmitter<string>();
  @Output() public onEdit: EventEmitter<string> = new EventEmitter<string>();
  @Output() public onDelete: EventEmitter<string> = new EventEmitter<string>();
  @Output() public onUpdateStatus: EventEmitter<string> =
    new EventEmitter<string>();
  @Output() public onInfo: EventEmitter<string> = new EventEmitter<string>();
  @Output() public onPageSizeChange: EventEmitter<any> =
    new EventEmitter<any>();
  @Output() public onPageChange: EventEmitter<number> =
    new EventEmitter<number>();

  public showConfirmDeleteModal: boolean = false;
  public showConfirmModal: boolean = false;

  public confirmMessage: string = '';
  public confirmAction: string = '';
  public selectedId: string = '';

  // Open confirm modal
  public openConfirmDeleteModal(id: string): void {
    const entity = this.data.items.find((x) => x.id === id);
    this.confirmAction = 'delete';
    this.confirmMessage = `Are you sure you want to ${this.confirmAction} ${entity?.username}?`;
    this.showConfirmDeleteModal = true;
    this.selectedId = id;
  }

  public openConfirmModal(id: string): void {
    const entity = this.data.items.find((x) => x.id === id);
    this.confirmAction = entity.isActive ? 'Lock' : 'Unlock';
    this.confirmMessage = `Are you sure you want to ${this.confirmAction} ${entity?.username}?`;
    this.showConfirmModal = true;
    this.selectedId = id;
  }

  // Confirm delete action
  public confirmDelete(): void {
    this.onDelete.emit(this.selectedId);
    this.closeConfirmModal();
  }

  public confirmUpdateStatus(): void {
    this.onUpdateStatus.emit(this.selectedId);
    this.closeConfirmModal();
  }

  // Close confirm modal
  public closeConfirmModal(): void {
    this.showConfirmDeleteModal = false;
    this.showConfirmModal = false;
    this.selectedId = '';
  }
}
