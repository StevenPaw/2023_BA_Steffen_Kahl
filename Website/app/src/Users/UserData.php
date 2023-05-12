<?php

namespace App\Users;

use SilverStripe\ORM\DataObject;
use SilverStripe\Forms\DropdownField;
use SilverStripe\Security\Permission;
use App\CharacterDatabase\CharacterPart;

/**
 * Class \App\Database\Experience
 *
 * @property string $Nickname
 * @property int $XP
 * @property string $UserKey
 * @property int $SelectedEyesID
 * @property int $SelectedMouthID
 * @property int $SelectedHairID
 * @property int $SelectedBottomID
 * @property int $SelectedTopID
 * @property int $SelectedHatID
 * @property int $SelectedSkinColorID
 * @property int $SelectedBackDecoID
 * @method \App\CharacterDatabase\CharacterPart SelectedEyes()
 * @method \App\CharacterDatabase\CharacterPart SelectedMouth()
 * @method \App\CharacterDatabase\CharacterPart SelectedHair()
 * @method \App\CharacterDatabase\CharacterPart SelectedBottom()
 * @method \App\CharacterDatabase\CharacterPart SelectedTop()
 * @method \App\CharacterDatabase\CharacterPart SelectedHat()
 * @method \App\CharacterDatabase\CharacterPart SelectedSkinColor()
 * @method \App\CharacterDatabase\CharacterPart SelectedBackDeco()
 * @method \SilverStripe\ORM\ManyManyList|\App\CharacterDatabase\CharacterPart[] AquiredCharacterParts()
 */
class UserData extends DataObject
{
    private static $db = [
        "Nickname" => "Varchar(255)",
        "XP" => "Int",
        "UserKey" => "Varchar(512)",
    ];

    private static $has_one = [
        "SelectedEyes" => CharacterPart::class,
        "SelectedMouth" => CharacterPart::class,
        "SelectedHair" => CharacterPart::class,
        "SelectedBottom" => CharacterPart::class,
        "SelectedTop" => CharacterPart::class,
        "SelectedHat" => CharacterPart::class,
        "SelectedSkinColor" => CharacterPart::class,
        "SelectedBackDeco" => CharacterPart::class,
    ];

    private static $many_many = [
        "AquiredCharacterParts" => CharacterPart::class,
    ];

    private static $summary_fields = [
        "ID" => "ID",
        "Nickname" => "Nickname",
        "XP" => "XP",
    ];

    private static $field_labels = [
        "Nickname" => "Nickname",
    ];

    private static $default_sort = "ID ASC";

    private static $table_name = "UserData";

    private static $singular_name = "UserData";
    private static $plural_name = "UserDatas";

    private static $url_segment = "userdata";

    public function getTitle()
    {
        return $this->Nickname;
    }

    public function onBeforeWrite()
    {
        parent::onBeforeWrite();
        $aquiredParts = CharacterPart::get()->filter("RequiredXP:LessThanOrEqual", $this->XP);
        if ($aquiredParts->count() > 0) {
            $this->AquiredCharacterParts()->addMany($aquiredParts->toArray());
        }
    }

    public function getCMSFields()
    {
        $fields = parent::getCMSFields();

        $fields->removeByName("SelectedEyes");
        $partsmap = CharacterPart::get()->filter('Type', "Eyes")->map('ID', 'Title');
        $fields->insertAfter('SelectedEyesID', new DropdownField('SelectedEyesID', 'Selected Eyes', $partsmap));

        $fields->removeByName("SelectedMouth");
        $partsmap = CharacterPart::get()->filter('Type', "Mouth")->map('ID', 'Title');
        $fields->insertAfter('SelectedMouthID', new DropdownField('SelectedMouthID', 'Selected Mouth', $partsmap));

        $fields->removeByName("SelectedHair");
        $partsmap = CharacterPart::get()->filter('Type', "Hair")->map('ID', 'Title');
        $fields->insertAfter('SelectedHairID', new DropdownField('SelectedHairID', 'Selected Hair', $partsmap));

        $fields->removeByName("SelectedBottom");
        $partsmap = CharacterPart::get()->filter('Type', "Bottom")->map('ID', 'Title');
        $fields->insertAfter('SelectedBottomID', new DropdownField('SelectedBottomID', 'Selected Bottom', $partsmap));

        $fields->removeByName("SelectedTop");
        $partsmap = CharacterPart::get()->filter('Type', "Top")->map('ID', 'Title');
        $fields->insertAfter('SelectedTopID', new DropdownField('SelectedTopID', 'Selected Top', $partsmap));

        $fields->removeByName("SelectedHat");
        $partsmap = CharacterPart::get()->filter('Type', "Hat")->map('ID', 'Title');
        $fields->insertAfter('SelectedHatID', new DropdownField('SelectedHatID', 'Selected Hat', $partsmap));

        $fields->removeByName("SelectedSkinColor");
        $partsmap = CharacterPart::get()->filter('Type', "SkinColor")->map('ID', 'Title');
        $fields->insertAfter('SelectedSkinColorID', new DropdownField('SelectedSkinColorID', 'Selected SkinColor', $partsmap));

        $fields->removeByName("SelectedBackDeco");
        $partsmap = CharacterPart::get()->filter('Type', "BackDeco")->map('ID', 'Title');
        $fields->insertAfter('SelectedBackDecoID', new DropdownField('SelectedBackDecoID', 'Selected BackDeco', $partsmap));

        return $fields;
    }

    public function canView($member = null)
    {
        return true;
    }

    public function canEdit($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canDelete($member = null)
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }

    public function canCreate($member = null, $context = [])
    {
        return Permission::check('CMS_ACCESS_NewsAdmin', 'any', $member);
    }
}
