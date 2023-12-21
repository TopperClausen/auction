<script setup>
import { computed, reactive, ref, watch } from 'vue';
import { useSessionStore } from '../stores/session'
import { useVuelidate } from '@vuelidate/core';
import { sameAs, required, minLength, email } from '@vuelidate/validators';
import { register } from '../services/userService'
import CustomInput from '../components/CustomInput.vue'
import SubmitButton from '../components/SubmitButton.vue';
import { useRouter } from 'vue-router'

const session = useSessionStore();
const router = useRouter();

let error = ref('');

const form = ref({
    email: '',
    password: '',
    repeatedPassword: ''
});

const $v = useVuelidate({
    email: { required, email: email, $autoDirty: true },
    password: { required, minLength: minLength(6), $autoDirty: true },
    repeatedPassword: { required, minLength: minLength(6), sameAsPassword: sameAs(form.value.password), $autoDirty: true }
}, form.value);

const update = (key, value) => {
    form.value[key] = value
} 

const submit = async () => {
    $v.value.$touch()
    if($v.$dirty) return;

    register(form.value.email, form.value.password).then(response => {
        session.setJwt(response.data.jwt);
        session.setUserID(response.data.userID);
        
        router.push({name: 'Dashboard'})
    }).catch(err => {
        error.value = err.response.data.error;
    });
}
</script>

<template>
    <div class="h-screen w-screen flex items-center justify-center">
        <div class="shadow-md w-1/2 p-5">
            <h3 class="text-xl"> Register </h3>
            <span class="text-red-700 font-light text-xm"> {{ error }} </span>
            <div class="space-y-5">
                <CustomInput 
                    placeholder="Email"
                    label="Email*"
                    type="email"
                    :model="form.email"
                    :errors="$v.email.$errors.map(item => item.$message)"
                    @input="value => update('email', value)" 
                />
                <CustomInput 
                    placeholder="Password"
                    label="Password*"
                    type="password"
                    :model="form.password"
                    :errors="$v.password.$errors.map(item => item.$message)"
                    @input="value => update('password', value)"
                />
                <CustomInput
                    placeholder="Repeat password"
                    label="Repeat password*"
                    type="password"
                    :model="form.repeatedPassword"
                    :errors="$v.repeatedPassword.$errors.map(item => item.$message)"
                    @input="value => update('repeatedPassword', value)"
                />
                <div class="flex justify-end">
                    <SubmitButton @click="submit" :disable="$v.$dirty"> Opret </SubmitButton>
                </div>
            </div>
        </div>
    </div>
</template>