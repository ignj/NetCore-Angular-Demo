import * as Sentry from "@sentry/browser";
import { ToastrManager } from 'ng6-toastr-notifications';
import { ErrorHandler, Inject, Injector, isDevMode } from "@angular/core";

export class AppErrorHandler implements ErrorHandler{

    constructor(@Inject(Injector) private injector: Injector){ }

    // Need to get ToastrManager from injector rather than constructor injection to avoid cyclic dependency error
    private get toastrManager(): ToastrManager {
        return this.injector.get(ToastrManager);
    }    

    handleError(error: any): void {        
        if (!isDevMode()){
            Sentry.captureException(error);    
        }
        else{
            throw error;
        }
        
        console.log(error);

        this.toastrManager.errorToastr('Unexpected error.', 'Oops!', { animate: null });        
    }    

    
}