import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ModalService {
  private modalState = new BehaviorSubject<string | null>(null);

  modal$ = this.modalState.asObservable();

  open(modalType: string) {
    this.modalState.next(modalType);
  }

  close() {
    this.modalState.next(null);
  }
}
