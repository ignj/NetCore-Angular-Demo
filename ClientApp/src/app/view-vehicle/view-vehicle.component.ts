import { PhotoService } from './../services/photo.service';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleService } from '../services/vehicle.service';
import { showReportDialog } from '@sentry/browser';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css']
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicle: any;
  vehicleId: number; 
  activeTab: string;
  photos: any[];

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private toastr: ToastrManager,
    private photoService: PhotoService,
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

    this.photoService.getPhoto(this.vehicleId)
      .subscribe(photos => this.photos = photos as any[]);

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

  uploadPhoto(){
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.photoService.upload(this.vehicleId, nativeElement.files[0])
      .subscribe(photo => {
        this.photos.push(photo);
      })
  }
}