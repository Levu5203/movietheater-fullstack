import { Component } from '@angular/core';
import { ModalService } from '../../../../services/modal.service';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faTimes } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-change-password',
  imports: [FontAwesomeModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
  public faTimes: IconDefinition = faTimes;

  constructor(private modalService: ModalService) {}

  closeModal() {
    this.modalService.close();
  }
}
