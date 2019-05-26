import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';

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
    private vehicleService: VehicleService,
    private toastr: ToastrManager
  ) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe((makes: any[]) =>
      this.makes = makes);

    this.vehicleService.getFeatures().subscribe((features: any[]) =>
      this.features = features);
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
                       .subscribe(
                         x => console.log(x),
                         err => {
                           this.toastr.errorToastr('Unexpected error.', 'Oops!', { animate: null });
                        });
  }
}
