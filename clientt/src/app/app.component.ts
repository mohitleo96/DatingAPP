import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  title = 'Mohit';

  //2nd step connection with backend webAPI-->1st step is in app.module.ts-->3rd step on program.cs
  User:any;
  constructor(private http:HttpClient)
  { }
  ngOnInit(): void {
    this.http.get('http://localhost:5036/api/AppUsers').subscribe({
    next: response=>this.User=response,
    error:error=>console.log(error),
    complete:()=>console.log('Leo Request as completed')
    })
  }

}
