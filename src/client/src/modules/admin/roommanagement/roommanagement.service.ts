import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RoomManagementService {
  private currentView = new BehaviorSubject<'list' | 'detail'>('list');
  private selectedRoom = new BehaviorSubject<any>(null);

  currentView$ = this.currentView.asObservable();
  selectedRoom$ = this.selectedRoom.asObservable();

  constructor() {}

  setCurrentView(view: 'list' | 'detail') {
    this.currentView.next(view);
  }

  getCurrentView() {
    return this.currentView.value;
  }

  setSelectedRoom(room: any) {
    this.selectedRoom.next(room);
  }

  getSelectedRoom() {
    return this.selectedRoom.asObservable();
  }

  goToList() {
    this.setCurrentView('list');
    this.setSelectedRoom(null);
  }

  goToDetail(room: any) {
    this.setCurrentView('detail');
    this.setSelectedRoom(room);
  }
}
