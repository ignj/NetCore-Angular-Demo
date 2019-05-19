import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class MakeService {

  constructor(private http: HttpClient) { }

  getMakes(){
    return this.http.get('/api/makes');      
  }
}
