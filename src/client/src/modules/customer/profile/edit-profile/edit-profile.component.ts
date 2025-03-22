import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { ModalService } from '../../../../services/modal.service';
import { ChangePasswordComponent } from '../change-password/change-password.component';

@Component({
  selector: 'app-edit-profile',
  imports: [FontAwesomeModule, ChangePasswordComponent],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent {
  public faCalendar: IconDefinition = faCalendar;

  constructor(private modalService: ModalService) {}
  public isShowModal: boolean = false;
  openModal() {
    this.isShowModal = true;
    this.modalService.open('changePassword');
  }
}
