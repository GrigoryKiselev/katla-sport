import { Component, OnInit } from '@angular/core';
import { OrderListItem } from '../models/order-list-item';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html'
})
export class OrderListComponent implements OnInit {

    orders: OrderListItem[];

  constructor(private orderService: OrderService) { }

  ngOnInit() {
    this.getOrders();
  }

  getOrders() {
    this.orderService.getOrders().subscribe(h => this.orders = h);
  }

  onDelete(orderId: number) {
    var order = this.orders.find(h => h.id == orderId);
    this.orderService.setOrderStatus(orderId, true).subscribe(c => order.isDeleted = true);
  }

  onRestore(orderId: number) {
    var order = this.orders.find(h => h.id == orderId);
    this.orderService.setOrderStatus(orderId, true).subscribe(c => order.isDeleted = false);
  }
}
