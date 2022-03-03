#include "baseObject.h"
#include "altv-cpp-api/SDK.h"
#include <Log.h>

using namespace alt;

void BaseObject_SetMetaData(alt::IBaseObject* baseObject, const char* key, alt::MValueConst* value) {
    baseObject->SetMetaData(key, value->Get()->Clone());
}

uint8_t BaseObject_HasMetaData(alt::IBaseObject* baseObject, const char* key) {
    return baseObject->HasMetaData(key);
}

void BaseObject_DeleteMetaData(alt::IBaseObject* baseObject, const char* key) {
    baseObject->DeleteMetaData(key);
}

alt::MValueConst* BaseObject_GetMetaData(alt::IBaseObject* baseObject, const char* key) {
    return new MValueConst(baseObject->GetMetaData(key));
}
