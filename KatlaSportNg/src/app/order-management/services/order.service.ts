import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Order } from '../models/order';
import { OrderListItem } from '../models/order-list-item';
import { TransactionListItem } from '../models/transaction-list-item';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private url = environment.apiUrl + 'api/orders/';

  constructor(private http: HttpClient) { }

  getOrders(): Observable<Array<OrderListItem>> {
    return this.http.get<Array<OrderListItem>>(this.url);
  }

  getOrder(orderId: number): Observable<Order> {
    return this.http.get<Order>(`${this.url}${orderId}`);
  }

  getTransactions(orderId: number): Observable<Array<TransactionListItem>> {
    return this.http.get<Array<TransactionListItem>>(`${this.url}${orderId}/transactions`);
  }

  addOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(`${this.url}`, order);
  }

  updateOrder(order: Order): Observable<Object> {
    return this.http.put<Object>(`${this.url}${order.id}`, order);
  }

  deleteOrder(orderId: number): Observable<Object> {
    return this.http.delete<Object>(`${this.url}${orderId}`);
  }

  setOrderStatus(orderId: number, deletedStatus: boolean): Observable<Object> {
    return this.http.put<Order>(`${this.url}${orderId}/status/${deletedStatus}`, null);
  }
}
