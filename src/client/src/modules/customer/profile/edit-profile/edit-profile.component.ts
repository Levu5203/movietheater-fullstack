import { Component } from '@angular/core';
import { ModalService } from '../../../../services/modal.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-profile',
  imports: [CommonModule],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent {
  constructor(private readonly modalService: ModalService) {}
  openModal() {
    this.modalService.open('changePassword');
  }
}
