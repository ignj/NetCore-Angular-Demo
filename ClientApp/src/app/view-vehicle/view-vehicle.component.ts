import { ToastrManager } from 'ng6-toastr-notifications';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../services/vehicle.service';
import { showReportDialog } from '@sentry/browser';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  vehicle: any;
  vehicleId: number; 
  activeTab: string;

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private toastr: ToastrManager,
    private vehicleService: VehicleService) { 

    route.params.subscribe(p => {  
      this.vehicleId = +p['id'];    
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return; 
      }
    });
  }

  ngOnInit() {
    this.activeTab = 'vehicle';

    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(        
        v => this.vehicle = v,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/vehicles']);
            return; 
          }
        });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  setVehicle(activeTab){
    this.activeTab = activeTab;
  }

  setPhotos(activeTab){
    this.activeTab = activeTab;
  }

  
}