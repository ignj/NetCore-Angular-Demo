import { SaveVehicle } from './../models/SaveVehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Vehicle } from '../models/vehicle';

@Injectable()
export class VehicleService {

  private readonly vehiclesEndpoint = '/api/vehicles/';

  constructor(private http: HttpClient) { }

  getMakes(){
    return this.http.get('/api/makes');      
  }

  getFeatures(){
    return this.http.get('/api/features');
  }

  create(vehicle){
    return this.http.post<Vehicle>(this.vehiclesEndpoint, vehicle);                
  }

  getVehicle(id){
    return this.http.get(this.vehiclesEndpoint + id);
  }

  getVehicles(filter){
    return this.http.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter));
  }

  update(vehicle: SaveVehicle){
    return this.http.put<Vehicle>(this.vehiclesEndpoint + vehicle.id, vehicle);
  }

  delete(id){
    return this.http.delete(this.vehiclesEndpoint + id);
  }

  /* AUX METHODS */

  toQueryString(obj){
    var parts = [];    
    for (var property in obj){
      var value = obj[property];
      if (value != null && value != undefined){        
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }

    return parts.join('&');
  }
}
