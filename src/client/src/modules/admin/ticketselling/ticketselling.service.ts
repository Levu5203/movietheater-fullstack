import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TicketSellingService {
  private currentView = new BehaviorSubject<'ticketselling' | 'select-seat' | 'payment'>('ticketselling');
  private selectedSeats = new BehaviorSubject<any[]>([]);
  private totalPrice = new BehaviorSubject<number>(0);

  constructor() { }

  setCurrentView(view: 'ticketselling' | 'select-seat' | 'payment') {
    this.currentView.next(view);
  }

  getCurrentView() {
    return this.currentView.asObservable();
  }

  setSelectedSeats(seats: any[]) {
    this.selectedSeats.next(seats);
  }

  getSelectedSeats() {
    return this.selectedSeats.asObservable();
  }

  setTotalPrice(price: number) {
    this.totalPrice.next(price);
  }

  getTotalPrice() {
    return this.totalPrice.asObservable();
  }
} 