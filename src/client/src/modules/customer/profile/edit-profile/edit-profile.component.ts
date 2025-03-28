import { Component } from '@angular/core';
import {
  FontAwesomeModule,
  IconDefinition,
} from '@fortawesome/angular-fontawesome';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';

import { ModalService } from '../../../../services/modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-profile',
  imports: [CommonModule, FontAwesomeModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent {
  public faCalendar: IconDefinition = faCalendar;


  constructor(private readonly modalService: ModalService) {}
  openModal() {
    this.modalService.open('changePassword');
  }
}
