import * as _ from 'underscore';
import { Vehicle } from './../models/vehicle';
import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators/switchMap';
import { SaveVehicle } from '../models/SaveVehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    },
    lastUpdate: ''
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastr: ToastrManager
  ) {

    //Get id from route
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'] || 0;
    })

   }

  ngOnInit() {

    //Set up the endpoints to call
    var sources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ]

    if (this.vehicle.id) 
      sources.push(this.vehicleService.getVehicle(this.vehicle.id))

    //Put the call asociated to the data lo load for the view. The requests go to the server in parallel
    //So the order doesn't matter
    Observable.forkJoin(sources)
              .subscribe(data => {
                this.makes = <any[]>data[0];
                this.features = <any[]>data[1];
                
                if (this.vehicle.id){
                  this.setVehicle(data[2] as Vehicle);
                  this.populateModels();
                }
              }, err => {
                if (err.status == 404){
                  this.router.navigate(['']);
                }
              });
  }

  private setVehicle(v: Vehicle){
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange(){
    this.populateModels();    
    //Clean the model after change in the make
    delete this.vehicle.modelId;
  }

  private populateModels(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId)
    //TODO: implement separate endpoint to get models for particular make
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId, $event){
    if ($event.target.checked){
      this.vehicle.features.push(featureId);
    }
    else{
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit(){
    var result$ = (this.vehicle.id) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle); 
    result$.subscribe(vehicle => {
      this.toastr.successToastr('Success', 'The vehicle was saved', { animate: null });
      this.router.navigate(['/vehicles/', vehicle.id])
    });
  }

  delete(){
    if (confirm("Are you sure?")){
      this.vehicleService.delete(this.vehicle.id)
                         .subscribe(x => {
                            this.router.navigate(['']);
                         });
    }
  }
}
