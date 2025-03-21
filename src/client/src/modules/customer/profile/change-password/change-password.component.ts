import { Component } from '@angular/core';
import { ModalService } from '../../../../services/modal.service';

@Component({
  selector: 'app-change-password',
  imports: [],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css',
})
export class ChangePasswordComponent {
  constructor(private modalService: ModalService) {}

  closeModal() {
    this.modalService.close();
  }
}
