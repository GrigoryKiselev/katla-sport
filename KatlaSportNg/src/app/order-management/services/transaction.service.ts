import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Transaction } from '../models/transaction';
import { TransactionListItem } from '../models/transaction-list-item';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private url = environment.apiUrl + 'api/transactions/';

  constructor(private http: HttpClient) { }

  getTransactions(): Observable<Array<TransactionListItem>> {
    return this.http.get<Array<TransactionListItem>>(`${this.url}getAll`);
  }

  getTransaction(transactionId: number): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.url}getOne/${transactionId}`);
  }

  addTransaction(transaction: Transaction): Observable<Transaction> {
    return this.http.post<Transaction>(`${this.url}create`,transaction);
  }

  updateTransaction (transaction: Transaction): Observable<Transaction> {
    return this.http.post<Transaction>(`${this.url}update/${transaction.transactionId}`,transaction);
  }

  deleteTransaction(transactionId: number): Observable<Object> {
    return this.http.post<Object>(`${this.url}delete/${transactionId}`,null);
  }

  setTransactionStatus(transactionId: number, deletedStatus: boolean): Observable<Object> {
    return this.http.post<Transaction>(`${this.url}setStatus/${transactionId}/status/${deletedStatus}`, null);
  }
}

