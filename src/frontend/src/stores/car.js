import { defineStore } from 'pinia';
import { all } from '../services/carService'
import { all as allBrands } from '../services/brandService'

export const useCarStore = defineStore('car', {
    state: () => {
        return {
            cars: [],
            brands: []
        }
    },
    actions: {
        populate(userID, jwt) {
            all(userID, jwt).then(response => {
                this.cars = response.data
            });
            allBrands().then(response => {
                this.brands = response.data;
            })

        }
    }
}, { persist: true })