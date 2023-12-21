<script setup>
import { ref } from 'vue';
import CustomInput from '../components/CustomInput.vue';
import SubmitButton from '../components/SubmitButton.vue';
import BrandsCombobox from '../components/BrandsCombobox.vue';
import { create } from '../services/carService';
import { useSessionStore } from '../stores/session';
import { useRouter } from 'vue-router';

const error = ref('');

const form = ref({
    brandId: -1,
    name: '',
    kilometersDriven: -1,
    kilometersPerLiter: -1,
    modelYear: 0,
    latestPlate: ''
});

const sessionStore = useSessionStore();
const router = useRouter();

const update = (key, value) => {
    form.value[key] = value
}

const submit = () => {
    create(
        sessionStore.jwt,
        form.value.brandId,
        sessionStore.userID,
        form.value.name,
        form.value.kilometersDriven,
        form.value.kilometersPerLiter,
        form.value.modelYear,
        form.value.latestPlate
    ).then(response => {
        router.push({ name: 'Dashboard' })
    }).catch(err => {
        error = err.response.data.error;
    })
}
</script>

<template>
    <div class="h-screen w-screen flex items-center justify-center">
        <div class="shadow-md w-1/2 p-5">
            <h3 class="text-xl"> Opret ny bil </h3>
            <span class="text-red-700 font-light text-xm"> {{ error }} </span>
            <div class="space-y-5">
                <BrandsCombobox @change="value => form.brandId = value" :required="true" />
                <CustomInput 
                    placeholder="Navn"
                    label="Navn *"
                    type="text"
                    :model="form.name"
                    @input="value => update('name', value)" 
                />
                <CustomInput 
                    placeholder="Kilometer kørt"
                    label="Kilometer kørt"
                    type="number"
                    :model="form.kilometersDriven"
                    @input="value => update('kilometersDriven', value)"
                />
                <CustomInput 
                    placeholder="Kilometer per liter"
                    label="Kilometer per liter"
                    type="number"
                    :model="form.kilometersPerLiter"
                    @input="value => update('kilometersPerLiter', value)"
                />
                <CustomInput 
                    placeholder="Årgang"
                    label="Årgang"
                    type="number"
                    :model="form.modelYear"
                    @input="value => update('modelYear', value)"
                />
                <CustomInput 
                    placeholder="Seneste nummerplade"
                    label="Seneste nummerplade"
                    type="number"
                    :model="form.latestPlate"
                    @input="value => update('latestPlate', value)"
                />
                <div class="flex justify-end">
                    <SubmitButton @click.stop="submit"> Opret </SubmitButton>
                </div>
            </div>
        </div>
    </div>
</template>