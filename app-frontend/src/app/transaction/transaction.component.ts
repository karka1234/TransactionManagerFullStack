import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';


@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  transaction = {
    account_id: '',
    amount: 0
  };

  data: any;
  
  transactions: any[] = [];

  apiUrl = 'http://localhost:5000';///https://literate-computing-machine-6w47gq6v9r2r9rp-5000.app.github.dev';

  header = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getTransactions("");
  }


  async getTransactions(account_id: string) {
    try {
      const transactions = await firstValueFrom(this.http.get<any[]>(`${this.apiUrl}/transactions`));
      this.transactions = transactions.map(transaction => ({
        transaction_id: transaction.transaction_id,
        account_id: transaction.account_id,
        amount: transaction.amount,
        balance: 0 
      }));

      this.transactions.reverse();

      if (account_id !== "") {
        await this.getAccountBalance(account_id);
      }

    } catch (error) {
      console.error('Error fetching transactions:', error);
    }
  }

  // Fetch account balance asynchronously
  async getAccountBalance(account_id: string) {
    try {
      const account = await firstValueFrom(this.http.get<any>(`${this.apiUrl}/accounts/${account_id}`));

      if (this.transactions.length > 0) {
        this.transactions[0].balance = account.balance;
      }
    } catch (error) {
      console.error('Error fetching account balance:', error);
    }
  }

  testConnection(){
    this.http.get<any[]>(`${this.apiUrl}/ping`, {
      headers: this.header }).subscribe({
      next: (response) => {
        this.data = response;
        console.log('pong');
      },
      error: (error) => {
        console.log('Error:', error);
      }
    });    
  }

  onSubmit() {
     //this.testConnection();      
    var currentAccountId = this.transaction.account_id;   
    this.http.post(`${this.apiUrl}/transactions`, this.transaction).subscribe({
      next: (response) => {
        console.log('Transaction created', response);
        this.transaction = { account_id: '', amount: 0 };    
        this.getTransactions(currentAccountId);                         
      },
      error: (error) => {
        console.error('Error on submitting transaction', error);
        if (error.status === 0) {
          console.error('Network error or CORS issue');
        } else {
          console.error('HTTP error:', error.status, error.message);
        }
      }});
  }
}
