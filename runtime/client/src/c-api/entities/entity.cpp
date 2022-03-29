#include "entity.h"
#include "../../../../cpp-sdk/SDK.h"
#include <Log.h>

using namespace alt;

uint16_t Entity_GetID(alt::IEntity* player) {
    return player->GetID();
}

alt::IWorldObject* Entity_GetWorldObject(alt::IEntity* entity) {
    return dynamic_cast<alt::IWorldObject*>(entity);
}

uint8_t Entity_GetTypeByID(alt::ICore* core, uint16_t id, uint8_t& type) {
    auto entity = core->GetEntityByID(id);
    if (entity.IsEmpty()) return false;
    type = static_cast<uint8_t>(entity->GetType());
    return true;
}


uint32_t Entity_GetModel(alt::IEntity* entity) {
    return entity->GetModel();
}

alt::IPlayer* Entity_GetNetOwner(alt::IEntity* entity) {
    auto owner = entity->GetNetworkOwner();
    if (owner.IsEmpty()) return nullptr;
    return owner.Get();
}

int32_t Entity_GetScriptID(alt::IEntity* entity) {
    return entity->GetScriptGuid();
}

void Entity_GetRotation(alt::IEntity* entity, vector3_t& rot) {
    auto vector = entity->GetRotation();
    rot.x = vector.roll;
    rot.y = vector.pitch;
    rot.z = vector.yaw;
}


uint8_t Entity_HasStreamSyncedMetaData(alt::IEntity* Entity, const char* key) {
    return Entity->HasStreamSyncedMetaData(key);
}

alt::MValueConst* Entity_GetStreamSyncedMetaData(alt::IEntity* Entity, const char* key) {
    return new MValueConst(Entity->GetStreamSyncedMetaData(key));
}


uint8_t Entity_HasSyncedMetaData(alt::IEntity* Entity, const char* key) {
    return Entity->HasSyncedMetaData(key);
}

alt::MValueConst* Entity_GetSyncedMetaData(alt::IEntity* Entity, const char* key) {
    return new MValueConst(Entity->GetSyncedMetaData(key));
}
