import { Component, EventEmitter, Output } from '@angular/core';
import { ModalService } from '../../../../services/modal.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-edit-profile',
  imports: [ChangePasswordComponent],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent {
  constructor(private modalService: ModalService) {}
  public isShowModal: boolean = false;
  openModal() {
    this.isShowModal = true;
    this.modalService.open('changePassword');
  }
}
