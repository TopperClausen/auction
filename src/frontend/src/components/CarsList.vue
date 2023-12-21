<script setup>
import { useCarStore } from '../stores/car'
import { useSessionStore } from '../stores/session'
import SubmitButton from './SubmitButton.vue';

const sessionStore = useSessionStore();
const carStore = useCarStore();

const reload = () => {
    carStore.populate(sessionStore.userID, sessionStore.jwt);
}

if (carStore.cars.length === 0) {
    reload();
}

const brandFromCar = (car) => {
    return carStore.brands.find(item => item.id === car.brandID);
} 

</script>

<template>
    <div>
        <SubmitButton @click="reload"> Reload </SubmitButton>
        <div v-for="car in carStore.cars">
            <div class="grid grid-cols-3">
                <div>
                    <img style="height: 100px" src="https://img.freepik.com/premium-vector/car-vector-illustration-classic-red-car-cartoon-transportation_648083-206.jpg?w=2000" />
                </div>
                <div>
                    <p> {{ brandFromCar(car)?.name }} </p>
                    <p>
                        {{ car.name }},
                        {{ car.modelYear }}
                    </p>
                </div>
                <div>
                    <p> {{ car.kilometersDriven }} km kørt </p>
                    <p>
                        {{ car.kilometersPerLiter }} /liter
                    </p>
                </div>
            </div>
        </div>
    </div>
</template>