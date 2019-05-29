import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators/switchMap';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastr: ToastrManager
  ) {

    //Get id from route
    route.params.subscribe(p => {
      this.vehicle.id = +p['id'];
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
                
                if (this.vehicle.id)
                  this.vehicle = data[2];
              }, err => {
                if (err.status == 404){
                  this.router.navigate(['']);
                }
              });
  }

  onMakeChange(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId)
    //TODO: implement separate endpoint to get models for particular make
    this.models = selectedMake ? selectedMake.models : [];
    
    //Clean the model after change in the make
    delete this.vehicle.modelId;
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
    this.vehicleService.create(this.vehicle)
                       .subscribe(x => console.log(x));
  }
}
