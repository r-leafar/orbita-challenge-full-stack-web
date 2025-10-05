// composables/useForm.ts
import { ref } from 'vue'

export function useForm<T extends Record<string, any>>(initialValues: T, rules: Record<string, Function[]>) {
    const values = ref({ ...initialValues })
    const formRef = ref<any>()

    const validate = async () => {
        if (!formRef.value) return { valid: false }
        return formRef.value.validate() // Vuetify v-form validate
    }

    const reset = () => {
        values.value = { ...initialValues }
    }

    return { values, formRef, rules, validate, reset }
}
